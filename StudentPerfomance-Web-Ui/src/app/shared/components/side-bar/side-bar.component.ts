import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss',
})
export class SideBarComponent {
  @ViewChildren('menuButton') menuButtons!: QueryList<
    ElementRef<HTMLButtonElement>
  >;

  protected selectMenu(event: Event, button: HTMLButtonElement) {
    this.menuButtons.forEach((buttonEl) => {
      buttonEl.nativeElement.classList.remove('focused');
    });
  }
}
