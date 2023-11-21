import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ObservatoriesComponent } from './observatories/observatories/observatories.component';
import { MeteorsComponent } from './meteors/meteors.component';
import { MeteorDetailComponent } from './meteors/components/meteor-detail/meteor-detail.component';

const routes: Routes = [
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
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
