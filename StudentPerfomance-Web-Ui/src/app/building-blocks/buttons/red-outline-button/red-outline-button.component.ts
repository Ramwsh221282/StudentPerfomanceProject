import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-red-outline-button',
  standalone: true,
  imports: [],
  templateUrl: './red-outline-button.component.html',
  styleUrl: './red-outline-button.component.scss',
})
export class RedOutlineButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
