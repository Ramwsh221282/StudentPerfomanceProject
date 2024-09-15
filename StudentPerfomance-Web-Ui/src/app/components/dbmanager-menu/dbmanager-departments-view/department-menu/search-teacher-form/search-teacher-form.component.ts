import { Component, OnInit } from '@angular/core';
import { BaseTeacherForm } from '../models/base-teacher-form';
import { ReactiveFormsModule } from '@angular/forms';
import { FacadeTeacherService } from '../services/facade-teacher.service';

@Component({
  selector: 'app-search-teacher-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search-teacher-form.component.html',
  styleUrl: './search-teacher-form.component.scss',
})
export class SearchTeacherFormComponent
  extends BaseTeacherForm
  implements OnInit
{
  public constructor(private readonly _facadeService: FacadeTeacherService) {
    super('Поиск преподавателя');
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected searchClick(): void {
    this._facadeService.filter(
      this.createTeacherFromForm(this._facadeService.currentDepartment)
    );
    this.initForm();
  }

  protected cancelClick(): void {
    this._facadeService.fetch();
  }
}
