import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HlmButtonImports } from '@spartan-ng/helm/button';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { HlmIconImports } from '@spartan-ng/helm/icon';
import { lucideUserCircle2 } from '@ng-icons/lucide';

@Component({
    selector: 'app-header',
    imports: [RouterLink, RouterLinkActive, HlmButtonImports, NgIcon, HlmIconImports],
    providers: [provideIcons({ lucideUserCircle2 })],
    templateUrl: './header.html',
    styleUrl: './header.css'
})
export class Header {

}
