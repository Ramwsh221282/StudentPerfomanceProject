import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-yellow-outline-button',
  standalone: true,
  imports: [],
  templateUrl: './yellow-outline-button.component.html',
  styleUrl: './yellow-outline-button.component.scss',
})
export class YellowOutlineButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick(): void {
    this.onClicked.emit();
  }
}
