import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResumoFinanceiroComponent } from './resumo-financeiro.component';

describe('ResumoFinanceiroComponent', () => {
  let component: ResumoFinanceiroComponent;
  let fixture: ComponentFixture<ResumoFinanceiroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResumoFinanceiroComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResumoFinanceiroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
