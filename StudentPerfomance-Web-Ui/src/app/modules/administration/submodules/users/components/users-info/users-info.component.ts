import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-users-info',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './users-info.component.html',
  styleUrl: './users-info.component.scss',
})
export class UsersInfoComponent {}
