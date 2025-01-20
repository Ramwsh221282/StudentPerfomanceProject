import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-greeting-page-block',
  standalone: true,
  imports: [],
  templateUrl: './greeting-page-block.component.html',
  styleUrl: './greeting-page-block.component.scss',
})
export class GreetingPageBlockComponent {
  @Input({ required: true }) blockImage: string = '';
  @Input({ required: true }) blockTitle: string = '';
  @Input({ required: true }) blockDescription: string = '';
}
