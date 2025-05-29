import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RecipeComponent } from "./components/recipe-component/recipe-component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, RecipeComponent],
    templateUrl: './app.html',
    styleUrl: './app.scss',
})
export class App {
    protected title = 'RecipeUI';
}
