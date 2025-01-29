import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormularioTransacaoComponent } from './formulario-transacao.component';

describe('FormularioTransacaoComponent', () => {
  let component: FormularioTransacaoComponent;
  let fixture: ComponentFixture<FormularioTransacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormularioTransacaoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormularioTransacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
