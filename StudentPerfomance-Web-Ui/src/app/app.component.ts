import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { setTheme } from 'ngx-bootstrap/utils';
import { SideBarComponent } from './shared/components/side-bar/side-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SideBarComponent, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  constructor() {
    setTheme('bs5');
  }
}
