import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SiteRoutingModule } from './site-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { PostComponent } from './post/post.component';
import { ResgistoComponent } from './resgisto/resgisto.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    LayoutComponent,
    HomeComponent,
    PostComponent,
    ResgistoComponent
  ],
  imports: [
    CommonModule,
    NgxPaginationModule,
    ReactiveFormsModule,
    SiteRoutingModule
  ],
  exports: [
    LayoutComponent
  ]
})
export class SiteModule { }
