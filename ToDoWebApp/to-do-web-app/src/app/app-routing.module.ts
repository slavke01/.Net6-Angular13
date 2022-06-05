import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ToDoListComponent } from './features/to-do-list/to-do-list/to-do-list.component';
import { ToDoListItemComponent } from './features/to-do-list/to-do-list-item/to-do-list-item.component';
import { AuthComponent } from './auth/auth.component';
import { AuthGuard } from '@auth0/auth0-angular';
import { ShareComponent} from "./features/to-do-list/share/share.component" 
const routes: Routes = [
  { path: '', component: AuthComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'to-do-list',
    component: ToDoListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'to-do-list/:id',
    component: ToDoListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'to-do-list/:shareid/share',
    component: ShareComponent,
  },
  {
    path: 'to-do-list-item',
    component: ToDoListItemComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'to-do-list-item/:itemid',
    component: ToDoListItemComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
