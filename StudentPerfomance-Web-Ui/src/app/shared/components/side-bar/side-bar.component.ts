import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../modules/users/services/auth.service';
import { NgClass, NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [RouterLink, NgIf, NgOptimizedImage, NgClass],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss',
})
export class SideBarComponent {
  protected readonly menuButtons: any = [
    {
      id: 1,
      label: '• Авторизация',
    },
    {
      id: 2,
      label: '• Личный кабинет',
    },
    {
      id: 3,
      label: '• Администрирование',
    },
    {
      id: 4,
      label: '• Отчёты контрольных недель',
    },
    {
      id: 5,
      label: '• Преподавателям',
    },
  ];
  protected activeButton: number = 0;

  public constructor(protected readonly authService: AuthService) {}
}
