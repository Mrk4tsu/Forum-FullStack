import {isPlatformBrowser} from "@angular/common";
import {WEB_DESCRIPTION, WEB_IMAGE, WEB_KEYWORDS, WEB_NAME} from '../constant';
import {inject, Injectable, PLATFORM_ID} from '@angular/core';
import {Meta, Title} from '@angular/platform-browser';
import {Router} from "@angular/router";
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SeoService {
  private domain = environment.baseDomain;
  private title = inject(Title);
  private meta = inject(Meta);
  private router = inject(Router);
  private platformId = inject(PLATFORM_ID);

  updateSeoWithKeyword(title: string | null, description: string | null, image: string | null, keywords: string | null) {
    const url = this.router.url;
    const finalTitle = title ? `${title} | ${WEB_NAME}` : WEB_NAME;
    const finalDescription = description || WEB_DESCRIPTION;
    const finalImage = image || WEB_IMAGE;
    const finalKeywords = `${keywords}, ${WEB_KEYWORDS}` || WEB_KEYWORDS;

    // Cập nhật title
    this.title.setTitle(finalTitle);

    this.meta.removeTag('name="canonical"');
    this.meta.addTag({rel: 'canonical', href: url});

    // Cập nhật meta tags
    this.meta.updateTag({property: 'description', content: finalDescription});
    this.meta.updateTag({name: 'keywords', content: finalKeywords});

    // Open Graph
    this.meta.updateTag({property: 'og:title', content: finalTitle});
    this.meta.updateTag({property: 'og:description', content: finalDescription});
    this.meta.updateTag({property: 'og:image', content: finalImage});
    this.meta.updateTag({property: 'og:url', content: this.domain + url});
    // Twitter Cards
    this.meta.updateTag({property: 'twitter:title', content: finalTitle});
    this.meta.updateTag({property: 'twitter:description', content: finalDescription});
    this.meta.updateTag({property: 'twitter:image', content: finalImage});
    if (isPlatformBrowser(this.platformId)) {
    }
  }

  updateSeo(title: string | null, description: string | null, image: string | null) {
    const url = this.router.url;
    const finalTitle = title ? `${title} | ${WEB_NAME}` : WEB_NAME;
    const finalDescription = description || WEB_DESCRIPTION;
    const finalImage = image || WEB_IMAGE;

    // Cập nhật title
    this.title.setTitle(finalTitle);

    this.meta.removeTag('name="canonical"');
    this.meta.addTag({rel: 'canonical', href: url});

    // Cập nhật meta tags
    this.meta.updateTag({property: 'description', content: finalDescription});

    // Open Graph
    this.meta.updateTag({property: 'og:title', content: finalTitle});
    this.meta.updateTag({property: 'og:description', content: finalDescription});
    this.meta.updateTag({property: 'og:image', content: finalImage});
    this.meta.updateTag({property: 'og:url', content: this.domain + url});
    // Twitter Cards
    this.meta.updateTag({property: 'twitter:title', content: finalTitle});
    this.meta.updateTag({property: 'twitter:description', content: finalDescription});
    this.meta.updateTag({property: 'twitter:image', content: finalImage});
    if (isPlatformBrowser(this.platformId)) {
    }
  }
}
