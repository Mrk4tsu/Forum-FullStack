import {Route, Routes, UrlMatchResult, UrlSegment, UrlSegmentGroup} from '@angular/router';
import {HomeComponent} from './page/home/home.component';
import {PageComponent} from './page/page.component';
import {ModComponent} from './page/mod/mod.component';
import {SocialComponent} from './page/social/social.component';
import {DownloadComponent} from './page/download/download.component';
import {ForbiddenComponent} from './shared/component/forbidden/forbidden.component';
import {PageNotFoundComponent} from './shared/component/page-not-found/page-not-found.component';
import {UploadComponent} from './page/upload/upload.component';
import {UpdateComponent} from './page/update/update.component';
import {authGuard} from './shared/services/helper/auth.guard';
import {claimReq} from './shared/services/helper/claimReq-utils';
import {NotifyComponent} from './page/notify/notify.component';
import {UpdateNotifyComponent} from './page/update-notify/update-notify.component';

export const routes: Routes = [
  {
    path: '', component: PageComponent,
    children: [
      {path: '', component: HomeComponent},
      {
        path: 'mod', component: ModComponent,
        data: {
          inputBindings: {
            page: {type: 'queryParam', name: 'page', defaultValue: 1},
            keyword: {type: 'queryParam', name: 'keyword', defaultValue: ''},
            username: {type: 'queryParam', name: 'tag', defaultValue: ''},
            isPrivate: {type: 'queryParam', name: 'server', defaultValue: ''},
          }
        }
      },
      {path: 'social', component: SocialComponent},
      {
        matcher: modRouteMatcher,
        component: DownloadComponent,
        data: {
          inputBindings: {
            page: {type: 'queryParam', name: 'page', defaultValue: 1},
          }
        }
      },
      {
        path: 'upload', component: UploadComponent,
        canActivate: [authGuard],
        data: {claimReq: claimReq.adminOnly}
      },
      {
        path: 'mod/:id/edit', component: UpdateComponent,
        canActivate: [authGuard],
        data: {claimReq: claimReq.adminOnly}
      },
      {
        path: 'notify', component: NotifyComponent,
        canActivate: [authGuard],
        data: {claimReq: claimReq.bossOnly}
      },
      {
        path: 'edit-notify/:id', component: UpdateNotifyComponent,
        canActivate: [authGuard],
        data: {claimReq: claimReq.bossOnly}
      },
      {path: 'forbidden', component: ForbiddenComponent},
      {path: '404', component: PageNotFoundComponent},
      {
        path: '**',
        component: PageNotFoundComponent
      }
    ]
  },

];

export function modRouteMatcher(segments: UrlSegment[], group: UrlSegmentGroup, route: Route): UrlMatchResult | null {
  // Đảm bảo đúng định dạng: /mod/username-productId
  if (segments.length === 2 && segments[0].path === 'mod') {
    const match = segments[1].path.match(/^([a-zA-Z0-9_]+)-(\d+)$/);
    if (match) {
      const usernameAuthor = match[1];
      const productId = match[2];

      return {
        consumed: segments,
        posParams: {
          usernameAuthor: new UrlSegment(usernameAuthor, {}),
          productId: new UrlSegment(productId, {}),
        },
      };
    }
  }

  return null;
}
