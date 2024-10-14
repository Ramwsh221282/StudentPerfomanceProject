import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentTeachersMenuModalComponent } from './department-teachers-menu-modal.component';

describe('DepartmentTeachersMenuModalComponent', () => {
  let component: DepartmentTeachersMenuModalComponent;
  let fixture: ComponentFixture<DepartmentTeachersMenuModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartmentTeachersMenuModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepartmentTeachersMenuModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
