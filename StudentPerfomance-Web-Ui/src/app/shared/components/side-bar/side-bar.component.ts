import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../modules/users/services/auth.service';
import { NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [RouterLink, NgIf, NgOptimizedImage],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss',
})
export class SideBarComponent {
  @ViewChildren('menuButton') menuButtons!: QueryList<
    ElementRef<HTMLButtonElement>
  >;

  public constructor(protected readonly authService: AuthService) {}
}
