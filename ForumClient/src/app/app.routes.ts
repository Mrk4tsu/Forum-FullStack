import {Routes} from '@angular/router';
import {PageComponent} from './page/page.component';
import {ForumComponent} from './page/forum/forum.component';
import {ListTopicComponent} from './page/forum/list-topic/list-topic.component';
import {TopicComponent} from './page/forum/topic/topic.component';
import {RegisterComponent} from './page/register/register.component';
import {LoginComponent} from './page/login/login.component';
import {ForbiddenComponent} from './shared/component/forbidden/forbidden.component';
import {PageNotFoundComponent} from './shared/component/page-not-found/page-not-found.component';
import {ForgotPasswordComponent} from './page/forgot-password/forgot-password.component';
import {ConfirmPasswordComponent} from './page/confirm-password/confirm-password.component';
import {ChangePasswordComponent} from './page/change-password/change-password.component';
import {authGuard} from './shared/services/helper/auth.guard';
import {NotificationComponent} from './page/forum/notification/notification.component';

export const routes: Routes = [
  {
    path: '', component: PageComponent,
    children: [
      {path: 'login', component: LoginComponent},
      {path: 'register', component: RegisterComponent},
      {path: 'forgot-password', component: ForgotPasswordComponent},
      {path: 'confirm-password', component: ConfirmPasswordComponent},
      {
        path: 'change-password', component: ChangePasswordComponent,
        canActivate: [authGuard]
      },
      {
        path: '', component: ForumComponent,
        children: [
          {path: '', component: ListTopicComponent},
          {
            path: 'topic/:id', component: TopicComponent,
            data: {
              inputBindings: {
                page: {type: 'queryParam', name: 'page', defaultValue: 1},
              }
            }
          },
          {
            path: 'notification/:id', component: NotificationComponent,
          },
        ]
      },
      {path: 'forbidden', component: ForbiddenComponent},
      {path: '404', component: PageNotFoundComponent},
    ],
    data: {
      inputBindings: {
        page: {type: 'queryParam', name: 'page', defaultValue: 1},
      }
    }
  }
];
