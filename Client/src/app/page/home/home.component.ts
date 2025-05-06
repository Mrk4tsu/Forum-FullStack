import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {SeoService} from '../../shared/services/seo.service';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrls: [
    './home.component.css',
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent {
  seo = inject(SeoService)
  router = inject(Router)

  constructor() {
    this.seo.updateSeoWithKeyword('Trang chủ', null, null, null);
  }
  goToDownload(param : string) {
   const url = `${environment.baseDomain}/${param}`;
    window.open(url);
  }
}
