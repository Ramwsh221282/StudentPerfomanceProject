import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-yellow-outline-button',
  imports: [],
  templateUrl: './yellow-outline-button.component.html',
  styleUrl: './yellow-outline-button.component.scss',
  standalone: true,
})
export class YellowOutlineButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
