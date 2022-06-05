import { Component, OnInit } from '@angular/core';
import { AuthService, } from '@auth0/auth0-angular';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  public name? =""
  constructor(public auth: AuthService) { }
  helper = new JwtHelperService();
  ngOnInit(): void {

    this.auth.user$.subscribe(
      (profile) => {
          this.name=profile?.name
          console.log(profile)
      });
    this.auth.getAccessTokenSilently().subscribe(token=>console.log(this.helper.decodeToken(token)));
  }

  logout(): void {
    this.auth.logout({ returnTo: 'http://localhost:4200' });
  }
}
