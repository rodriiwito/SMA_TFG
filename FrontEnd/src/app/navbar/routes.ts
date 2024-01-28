import { Route } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { ObservatoriesComponent } from '../observatories/observatories/observatories.component';
import { MeteorsComponent } from '../meteors/meteors.component';
import { MeteorDetailComponent } from '../meteors/components/meteor-detail/meteor-detail.component';
import { UserComponent } from '../user/user.component';

export const USER_ROUTES: Route[] = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'observatories',
    component: ObservatoriesComponent,
  },
  {
    path: 'meteors',
    component: MeteorsComponent,
  },
  {
    path: 'meteors/:id',
    component: MeteorDetailComponent,
  },
  {
    path: 'user',
    component: UserComponent,
  },
];
