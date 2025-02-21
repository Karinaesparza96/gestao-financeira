import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmacaoExcluirComponent } from './confirmacao-excluir.component';

describe('ConfirmacaoExcluirComponent', () => {
  let component: ConfirmacaoExcluirComponent;
  let fixture: ComponentFixture<ConfirmacaoExcluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfirmacaoExcluirComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmacaoExcluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
