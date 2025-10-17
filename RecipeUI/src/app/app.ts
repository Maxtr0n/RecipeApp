import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit, Signal, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthenticatedResult, OidcSecurityService, UserDataResult } from 'angular-auth-oidc-client';

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, AsyncPipe],
    templateUrl: './app.html',
    styleUrl: './app.scss'
})
export class App implements OnInit {
    protected readonly oidcSecurityService = inject(OidcSecurityService);
    protected readonly title = signal('RecipeUI');
    authenticated: Signal<AuthenticatedResult> = this.oidcSecurityService.authenticated;
    userData: Signal<UserDataResult> = this.oidcSecurityService.userData;

    ngOnInit(): void {
        this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated }) => {
            console.log('App is authenticated:', isAuthenticated);
        });
    }

    login() {
        this.oidcSecurityService.authorize();
    }

    logout() {
        this.oidcSecurityService.logoff().subscribe((result) => console.log(result));
    }
}
