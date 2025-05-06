import { ChangeDetectorRef, Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ModService } from '../../shared/services/mod.service';
import { BehaviorSubject } from 'rxjs';
import { Mod, ModWithImage } from '../../shared/model/mod.interface';
import { PaginationComponent } from '../../shared/component/pagination/pagination.component';
import { SeoService } from '../../shared/services/seo.service';

@Component({
  selector: 'app-mod',
  imports: [
    CommonModule,
    RouterLink,
    PaginationComponent
  ],
  templateUrl: './mod.component.html',
  styleUrl: './mod.component.css'
})
export class ModComponent implements OnInit, OnChanges {
  @Input() page: number = 1;
  @Input() keyword: string = '';
  @Input() tag: string = '';
  @Input() server: any | null = null;

  tags = [
    { name: 'manhhdc', value: 'manhhdc', isActive: false },
    { name: 'gatapchoi', value: 'gatapchoi', isActive: false },
    { name: 'ehvn', value: 'ehvn', isActive: false },
    { name: 'hunghero', value: 'hunghero', isActive: false },
  ];
  tagsServer = [
    { name: 'teamobi', query: 'teamobi', isActive: false },
    { name: 'server lậu', query: 'private', isActive: false },
  ];
  tagsDevice = [
    { name: 'android', isActive: true },
    { name: 'ios', isActive: false },
    { name: 'pc', isActive: false },
    { name: 'java', isActive: false },
  ];
  private images: string[] = [
    'assets/images/utilities/dragon-boy.jpg',
    'assets/images/utilities/dragon-boy-1.png',
    'assets/images/utilities/dragon-boy-2.png'
  ];
  modService = inject(ModService);
  cdr = inject(ChangeDetectorRef);
  router = inject(Router);
  seo = inject(SeoService);

  modSubject = new BehaviorSubject<ModWithImage[]>([]);
  mod$ = this.modSubject.asObservable();

  isLoading = false;
  paginationData = {
    currentPage: 1,
    totalPages: 1,
    totalRecords: 0
  };

  ngOnInit() {
    const description = 'Danh sách mod game mobile, mod game android, mod game ios, mod game pc, mod game java. Tải về và cài đặt ngay!';
    this.seo.updateSeoWithKeyword('Danh sách mod', description, null, null);
    this.loadMods()
    this.updateActiveTags();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['keyword'] || changes['tag'] || changes['server']) {
      this.updateActiveTags(); // Cập nhật khi username thay đổi
    }
    if (changes['page'] || changes['keyword'] || changes['tag'] || changes['server']) {
      this.loadMods();
    }
  }

  onTagNameChange(tag: string): void {
    const newUsername = this.tag === tag ? '' : tag;
    this.router.navigate(['/mod'], {
      queryParams: {
        tag: newUsername,
        page: 1
      },
      queryParamsHandling: 'merge'
    });
    this.tag = tag;
  }

  onTagCategoryChange(tag: string): void {
    // Nếu tag đã active, click lại sẽ bỏ chọn
    const newCategory = this.keyword === tag ? '' : tag;
    this.router.navigate(['/mod'], {
      queryParams: {
        keyword: newCategory,
        page: 1 // Reset về trang đầu khi đổi tag
      },
      queryParamsHandling: 'merge'
    });
    this.keyword = tag;
  }

  onTagServerChange(query: string): void {
    const newServer = this.server === query ? undefined : query;

    this.router.navigate(['/mod'], {
      queryParams: {
        server: newServer,
        page: 1
      },
      queryParamsHandling: 'merge'
    });
  }


  onPageChange(newPage: number) {
    const queryParams: any = { page: newPage };
    this.router.navigate(['/mod'], {
      queryParams,
      queryParamsHandling: 'merge'
    });
  }

  private updateActiveTags(): void {
    const activeTagName = this.tag;
    const keywordTagName = this.keyword;
    const isServerTagName = this.server;
    this.tags.forEach(tag => {
      tag.isActive = tag.value === activeTagName;
    });
    this.tagsDevice.forEach(tag => {
      tag.isActive = tag.name === keywordTagName;
    });
    this.tagsServer.forEach(tag => {
      tag.isActive = tag.query === isServerTagName;
    });
  }

  loadMods() {
    this.isLoading = true;
    this.modService.getMods(this.server, this.page, 10, this.tag, this.keyword).subscribe({
      next: (result) => {
        if (result.success && result.data) {
          const modsWithImages: ModWithImage[] = result.data.items.map((mod: Mod, index: number) => ({
            ...mod,
            image: this.images[Math.abs(mod.id) % this.images.length]
          }));
          this.modSubject.next(modsWithImages)
          this.paginationData = {
            currentPage: result.data.pageIndex,
            totalPages: Math.ceil(result.data.totalRecords / result.data.pageSize),
            totalRecords: result.data.totalRecords
          };
          this.cdr.detectChanges()
        }
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }
  getRandomImage(): string {
    const randomIndex = Math.floor(Math.random() * this.images.length);
    return this.images[randomIndex];
  }
}
