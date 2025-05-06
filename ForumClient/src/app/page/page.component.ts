import {Component} from '@angular/core';
import {Router, RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-page',
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './page.component.html',
  styleUrl: './page.component.css'
})
export class PageComponent {
  dateNow: number = new Date().getFullYear();

  constructor(public router: Router) {
    //Lấy duy nhất năm hiện tại
  }

  isForumActive(): boolean {
    return this.router.url.startsWith('/') || this.router.url.startsWith('/topic');
  }

  isModActive(): boolean {
    return this.router.url.startsWith('/mod') || this.router.url.startsWith('/download');
  }

  onNavigate(event: Event): void {
    const url = (event.target as HTMLSelectElement).value;
    if (url) {
      window.open(url, '_blank');
    }
  }
}
