import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-blue-button',
  imports: [],
  templateUrl: './blue-button.component.html',
  styleUrl: './blue-button.component.scss',
  standalone: true,
})
export class BlueButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
