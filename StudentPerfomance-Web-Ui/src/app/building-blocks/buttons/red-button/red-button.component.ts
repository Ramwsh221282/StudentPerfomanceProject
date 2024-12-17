import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-red-button',
  standalone: true,
  imports: [],
  templateUrl: './red-button.component.html',
  styleUrl: './red-button.component.scss',
})
export class RedButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
