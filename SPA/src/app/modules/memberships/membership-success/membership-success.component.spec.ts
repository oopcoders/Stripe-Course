import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembershipSuccessComponent } from './membership-success.component';

describe('MembershipSuccessComponent', () => {
  let component: MembershipSuccessComponent;
  let fixture: ComponentFixture<MembershipSuccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MembershipSuccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MembershipSuccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
