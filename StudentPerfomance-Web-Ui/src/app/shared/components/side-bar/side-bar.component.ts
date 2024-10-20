import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../modules/users/services/auth.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [RouterLink, NgIf],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss',
})
export class SideBarComponent {
  @ViewChildren('menuButton') menuButtons!: QueryList<
    ElementRef<HTMLButtonElement>
  >;

  public constructor(protected readonly authService: AuthService) {}

  protected selectMenu(event: Event, button: HTMLButtonElement) {
    this.menuButtons.forEach((buttonEl) => {
      buttonEl.nativeElement.classList.remove('focused');
    });
  }
}
