import { Component } from '@angular/core';
import { GreetingPageHeaderComponent } from './greeting-page-header/greeting-page-header.component';
import { GreetingPageBlockComponent } from './greeting-page-block/greeting-page-block.component';
import { GreetingPagePanelComponent } from './greeting-page-panel/greeting-page-panel.component';

@Component({
  selector: 'app-greeting-page',
  standalone: true,
  imports: [
    GreetingPageHeaderComponent,
    GreetingPageBlockComponent,
    GreetingPagePanelComponent,
  ],
  templateUrl: './greeting-page.component.html',
  styleUrl: './greeting-page.component.scss',
})
export class GreetingPageComponent {}
