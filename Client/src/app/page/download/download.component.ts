import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  inject,
  Input, OnChanges,
  OnDestroy,
  OnInit,
  PLATFORM_ID, SimpleChanges
} from '@angular/core';
import {CommonModule, isPlatformBrowser} from '@angular/common';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {TopicService} from '../../shared/services/topic.service';
import {AuthService} from '../../shared/services/auth.service';
import {TokenService} from '../../shared/services/token.service';
import {JwtPayload} from '../../shared/model/token.interface';
import {Topic, TopicDetail} from '../../shared/model/topic.interface';
import {HttpClient} from '@angular/common/http';
import {ModService} from '../../shared/services/mod.service';
import {BehaviorSubject, distinctUntilChanged, filter, Subject, Subscription, takeUntil} from 'rxjs';
import {ModDetail, React} from '../../shared/model/mod.interface';
import {DomSanitizer, SafeHtml} from '@angular/platform-browser';
import {PaginationComponent} from '../../shared/component/pagination/pagination.component';
import {SeoService} from '../../shared/services/seo.service';

@Component({
  selector: 'app-download',
  imports: [
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    PaginationComponent
  ],
  templateUrl: './download.component.html',
  styleUrls: [
    './download.component.css',
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DownloadComponent implements OnInit, OnDestroy, OnChanges {
  @Input() productId: number | null = null;
  @Input() usernameAuthor = '';
  @Input() page: number = 1;
  isBrowser = isPlatformBrowser(inject(PLATFORM_ID))
  authService = inject(AuthService)
  tokenService = inject(TokenService)
  router = inject(Router)
  isAuthorize = this.authService.isAuthenticated();
  modService = inject(ModService);
  seo = inject(SeoService);
  cdr = inject(ChangeDetectorRef);

  userInfo: JwtPayload | null = null;
  sanitizedContent: SafeHtml | null = null;

  private modSubject = new BehaviorSubject<ModDetail | null>(null);
  modDetail$ = this.modSubject.asObservable().pipe(
    filter(mod => mod !== null),
    distinctUntilChanged()
  );
  private reactSubject = new BehaviorSubject<React[]>([]);
  reacts$ = this.reactSubject.asObservable().pipe(
    filter(reacts => reacts.length > 0),
    distinctUntilChanged()
  );
  private destroy$ = new Subject<void>();

  formatDateTime: string = '';
  isOwner: boolean = false;
  isSending: boolean = false;
  isLoading: boolean = false;
  form = this.formBuilder.group({
    modId: [this.productId, Validators.required],
    content: ['', Validators.required],
  })
  paginationData = {
    currentPage: 1,
    totalPages: 1,
    totalRecords: 0
  };

  constructor(private sanitizer: DomSanitizer, private formBuilder: FormBuilder) {
    if (!this.isBrowser) return;
  }

  ngOnInit() {
    this.seo.updateSeoWithKeyword('Tải MOD', null, null, null);
    if (this.productId) {
      this.loadModDetail(Number(this.productId));
      this.loadModReacts(Number(this.productId));
    }
    if (this.isAuthorize) {
      const accessToken = this.tokenService.accessToken;
      if (accessToken) {
        this.userInfo = this.tokenService.getUserInfoFromToken(accessToken);
      }
      this.modSubject.pipe(
        takeUntil(this.destroy$)
      ).subscribe(mod => {
        if (mod) {
          this.isOwner = Number(this.userInfo!.sub) == mod.authorId;
        }
      });
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['page']) {
      this.loadModReacts(this.productId!);
    }
  }

  onPageChange(newPage: number) {
    const queryParams: any = {page: newPage};
    this.router.navigate(['/download', this.productId], {
      queryParams,
      queryParamsHandling: 'merge'
    });
  }

  deleteMod(id: number) {
    this.modService.deleteMod(id).subscribe({
      next: (data) => {
        if (data.success) {
          this.router.navigate(['/mod']);
        } else {
          console.error('Failed to delete mod:', data.message);
        }
      },
      error: (err) => {
        console.error('Failed to delete mod:', err);
      }
    });
  }

  deleteReact(id: number) {
    this.modService.deleteReact(id).subscribe({
      next: (data) => {
        if (data.success) {
          this.loadModReacts(this.productId!);
        } else {
          console.error('Failed to delete react:', data.message);
        }
      },
      error: (err) => {
        console.error('Failed to delete react:', err);
      }
    });
  }

  private loadModDetail(id: number): void {
    this.isLoading = true;
    this.modService.getModById(id).pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      next: (data) => {
        if (data.success) {
          this.isLoading = false;
          this.sanitizedContent = this.sanitizer.bypassSecurityTrustHtml(data.data.content);
          this.formatDateTime = this.modService.formatDateTime(data.data.createdAt);
          this.modSubject.next(data.data);
          this.cdr.markForCheck()
        } else this.router.navigate(['/404'])
      },
      error: (err) => this.router.navigate(['/404'])
    });
  }

  private loadModReacts(id: number): void {
    this.modService.getReacts(id, this.page).pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      next: (result) => {
        if (result.success) {
          this.paginationData = {
            currentPage: result.data.pageIndex,
            totalPages: Math.ceil(result.data.totalRecords / result.data.pageSize),
            totalRecords: result.data.totalRecords
          };
          this.reactSubject.next(result.data.items)
        }
      },
      error: (err) => console.error('Failed to load mod reacts:', err)
    })
  }

  onReactSubmit() {
    if (!this.isAuthorize) return;
    if (this.productId) {
      this.form.patchValue({modId: this.productId});
      const formValue = this.form.value;
      if (this.form.valid) {
        this.modService.createReact(formValue).subscribe({
          next: (res) => {
            this.isSending = false;
            this.form.reset();
            this.loadModReacts(this.productId!);
          },
          error: (err) => {
            this.isSending = false;
            console.error('Error creating reply:', err);
          }
        });
      }
    }
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
