import { Component } from '@angular/core';
import { CreateFormComponent } from './create-form/create-form.component';
import { ManageFormComponent } from './manage-form/manage-form.component';
import { SearchFormComponent } from './search-form/search-form.component';

@Component({
  selector: 'app-dbmanager-semesters-control-panel',
  standalone: true,
  imports: [CreateFormComponent, ManageFormComponent, SearchFormComponent],
  templateUrl: './dbmanager-semesters-control-panel.component.html',
  styleUrl: './dbmanager-semesters-control-panel.component.scss',
})
export class DbmanagerSemestersControlPanelComponent {}
