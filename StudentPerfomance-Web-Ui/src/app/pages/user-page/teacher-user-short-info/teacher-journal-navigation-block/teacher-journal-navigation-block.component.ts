import { Component } from '@angular/core';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { NgOptimizedImage } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-teacher-journal-navigation-block',
  imports: [BlueButtonComponent, NgOptimizedImage],
  templateUrl: './teacher-journal-navigation-block.component.html',
  styleUrl: './teacher-journal-navigation-block.component.scss',
  standalone: true,
})
export class TeacherJournalNavigationBlockComponent {
  public constructor(private readonly router: Router) {}

  public navigate(): void {
    this.router.navigate(['teacher-assignments']);
  }
}
