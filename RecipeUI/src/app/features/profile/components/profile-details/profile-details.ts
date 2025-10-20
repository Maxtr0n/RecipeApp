import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit, Signal } from '@angular/core';
import { AuthenticatedResult, OidcSecurityService, UserDataResult } from 'angular-auth-oidc-client';

@Component({
    selector: 'app-profile-details',
    imports: [AsyncPipe],
    templateUrl: './profile-details.html',
    styleUrl: './profile-details.css'
})
export class ProfileDetails implements OnInit {
    protected readonly oidcSecurityService = inject(OidcSecurityService);

    authenticated: Signal<AuthenticatedResult> = this.oidcSecurityService.authenticated;
    userData: Signal<UserDataResult> = this.oidcSecurityService.userData;

    ngOnInit(): void {
        this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated }) => {
            console.log('App is authenticated:', isAuthenticated);
        });
    };

    login() {
        this.oidcSecurityService.authorize();
    }

    logout() {
        this.oidcSecurityService.logoff().subscribe((result) => console.log(result));
    }
}
