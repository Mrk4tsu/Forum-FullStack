import {Component, inject} from '@angular/core';
import {SeoService} from '../../shared/services/seo.service';

@Component({
  selector: 'app-social',
  imports: [],
  templateUrl: './social.component.html',
  styleUrl: './social.component.css'
})
export class SocialComponent {
  seo = inject(SeoService)

  constructor() {
    const description = 'Thông tin trang web, thông tin về các dịch vụ, thông tin về các sản phẩm của chúng tôi. Chúng tôi cung cấp các dịch vụ tốt nhất cho bạn.';
    this.seo.updateSeoWithKeyword('Thông tin trang web', description, null, null);
  }
}
