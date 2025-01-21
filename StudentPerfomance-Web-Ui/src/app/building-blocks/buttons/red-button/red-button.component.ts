import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-red-button',
  imports: [],
  templateUrl: './red-button.component.html',
  styleUrl: './red-button.component.scss',
  standalone: true,
})
export class RedButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
