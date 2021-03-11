import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembershipFailureComponent } from './membership-failure.component';

describe('MembershipFailureComponent', () => {
  let component: MembershipFailureComponent;
  let fixture: ComponentFixture<MembershipFailureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MembershipFailureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MembershipFailureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
