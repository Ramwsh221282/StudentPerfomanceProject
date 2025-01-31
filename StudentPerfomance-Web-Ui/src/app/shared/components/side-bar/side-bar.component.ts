import { Component, EventEmitter, Output } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../pages/user-page/services/auth.service';
import { NgClass, NgIf, NgOptimizedImage } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-side-bar',
  imports: [RouterLink, NgIf, NgOptimizedImage, NgClass],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss',
  standalone: true,
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class SideBarComponent {
  @Output() menuHide: EventEmitter<boolean> = new EventEmitter();
  public isHidden: boolean = false;

  protected readonly menuButtons: any = [
    {
      id: 1,
      label: 'Авторизация',
    },
    {
      id: 2,
      label: 'Личный кабинет',
    },
    {
      id: 3,
      label: 'Администрирование',
    },
    {
      id: 4,
      label: 'Отчёты контрольных недель',
    },
    {
      id: 5,
      label: 'Преподавателям',
    },
  ];
  protected activeButton: number = 0;

  public constructor(
    protected readonly authService: AuthService,
    private readonly _router: Router,
  ) {}

  public hideMenu(): void {
    this.isHidden = !this.isHidden;
    this.menuHide.emit(this.isHidden);
  }

  public navigateMainPage($event: MouseEvent): void {
    this._router.navigate(['']);
  }
}
