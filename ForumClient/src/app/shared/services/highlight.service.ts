// src/app/shared/services/highlight.service.ts
import {inject, Injectable, PLATFORM_ID} from '@angular/core';
import {DomSanitizer, SafeHtml} from '@angular/platform-browser';
import hljs from 'highlight.js';
import 'highlight.js/styles/github.css';
import {isPlatformBrowser} from '@angular/common'; // hoặc chọn theme khác

@Injectable({
  providedIn: 'root'
})
export class HighlightService {
  isBrowser: boolean = isPlatformBrowser(inject(PLATFORM_ID));

  constructor(private sanitizer: DomSanitizer) {
    if (!this.isBrowser) return
    // Cấu hình highlight.js
    hljs.configure({
      languages: ['typescript', 'javascript', 'html', 'css', 'json', 'bash', 'css']
    });
  }

  highlight(content: string): SafeHtml {
    // Tạo một div tạm để parse HTML
    const tempDiv = document.createElement('div');
    tempDiv.innerHTML = content;

    // Tìm và highlight tất cả các thẻ <pre><code>
    const codeBlocks = tempDiv.querySelectorAll('pre code');
    codeBlocks.forEach((block: any) => {
      hljs.highlightElement(block);
    });

    // Thêm class cho các inline code
    const inlineCodes = tempDiv.querySelectorAll('code:not(pre code)');
    inlineCodes.forEach((code: any) => {
      code.classList.add('inline-code');
    });

    return this.sanitizer.bypassSecurityTrustHtml(tempDiv.innerHTML);
  }
}
