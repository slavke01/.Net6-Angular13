import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ToDoPreviewComponent } from './features/dashboard/to-do-preview/to-do-preview.component';
import { ToDoListModule } from './features/to-do-list/to-do-list.module';
import { NavigationComponent } from './shared/navigation/navigation.component';
import { SearchComponent } from './shared/navigation/search/search.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AuthModule } from '@auth0/auth0-angular';
import { environment as env } from '../environments/environment';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthHttpInterceptor } from '@auth0/auth0-angular';
@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ToDoPreviewComponent,
    NavigationComponent,
    SearchComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ToDoListModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    DragDropModule,
    AuthModule.forRoot({
      ...env.auth,
      httpInterceptor: {
        allowedList: [
          'https://localhost:7106/api/ToDoList',
          'https://localhost:7106/GetById',
          'https://localhost:7106/share',
          'https://localhost:7106/UpdateReminder',
          'https://localhost:7106/AddListItemToList',
          'https://localhost:7106/api/ToDoListItemControler',
        ],
      },
    }),
  ],
  exports: [BrowserModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
