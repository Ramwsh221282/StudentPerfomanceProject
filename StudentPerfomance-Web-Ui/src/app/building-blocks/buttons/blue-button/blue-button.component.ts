import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-blue-button',
  standalone: true,
  imports: [],
  templateUrl: './blue-button.component.html',
  styleUrl: './blue-button.component.scss',
})
export class BlueButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick(): void {
    this.onClicked.emit();
  }
}
