import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { EducationPlanSemester } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';

@Component({
  selector: 'app-semester-item',
  imports: [NgOptimizedImage, NgClass],
  templateUrl: './semester-item.component.html',
  styleUrl: './semester-item.component.scss',
  standalone: true,
})
export class SemesterItemComponent implements OnInit {
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) isSelectedSemester: boolean;
  @Output() selectSemester: EventEmitter<EducationPlanSemester> =
    new EventEmitter();
  public countOfDisciplinesWithoutTeacher: number = 0;

  public ngOnInit() {
    this.countOfDisciplinesWithoutTeacher =
      this.getAmountOfDisciplinesWithoutTeacher();
  }

  private getAmountOfDisciplinesWithoutTeacher(): number {
    let number = 0;
    for (const discipline of this.semester.disciplines) {
      if (discipline.teacher == null) number++;
    }
    return number;
  }
}
