import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-blue-outline-button',
  standalone: true,
  imports: [],
  templateUrl: './blue-outline-button.component.html',
  styleUrl: './blue-outline-button.component.scss',
})
export class BlueOutlineButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
