import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-green-outline-button',
  standalone: true,
  imports: [],
  templateUrl: './green-outline-button.component.html',
  styleUrl: './green-outline-button.component.scss',
})
export class GreenOutlineButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
