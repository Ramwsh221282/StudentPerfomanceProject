import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SearchStudentForm } from './search-student-form';
import { FacadeStudentService } from '../services/facade-student.service';

@Component({
  selector: 'app-search-student-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search-student-form.component.html',
  styleUrl: './search-student-form.component.scss',
})
export class SearchStudentFormComponent
  extends SearchStudentForm
  implements OnInit
{
  public constructor(private readonly _facadeService: FacadeStudentService) {
    super();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected searchClick(): void {
    this._facadeService.filterData(
      this.createStudentFromForm(this._facadeService.currentGroup)
    );
    this.ngOnInit();
  }

  protected cancelClick(): void {
    this._facadeService.fetchData();
    this.ngOnInit();
  }
}
