import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-yellow-button',
  imports: [],
  templateUrl: './yellow-button.component.html',
  styleUrl: './yellow-button.component.scss',
  standalone: true,
})
export class YellowButtonComponent {
  @Input() label: string = '';
  @Output() onClicked = new EventEmitter<void>();

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
