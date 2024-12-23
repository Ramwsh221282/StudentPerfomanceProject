import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-yellow-button',
  standalone: true,
  imports: [],
  templateUrl: './yellow-button.component.html',
  styleUrl: './yellow-button.component.scss',
})
export class YellowButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
