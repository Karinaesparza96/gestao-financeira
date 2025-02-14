import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraficoTransacaoComponent } from './grafico-transacao.component';

describe('GraficoTransacaoComponent', () => {
  let component: GraficoTransacaoComponent;
  let fixture: ComponentFixture<GraficoTransacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GraficoTransacaoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GraficoTransacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
