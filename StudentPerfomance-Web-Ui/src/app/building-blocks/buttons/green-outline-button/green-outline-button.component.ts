import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-green-outline-button',
  imports: [],
  templateUrl: './green-outline-button.component.html',
  styleUrl: './green-outline-button.component.scss',
  standalone: true,
})
export class GreenOutlineButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
