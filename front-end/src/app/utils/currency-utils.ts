export class CurrencyUtils {

    public static StringParaDecimal(input: any): any {
        if (input === null) return 0;

        input = input.replace(/\./g, '');
        input = input.replace(/,/g, '.');
        return parseFloat(input);
    }

    public static DecimalParaString(input: any): any {
        var ret = (input) ? input.toFixed(2).toString().replace(".", ",") : null;
        if (ret) {
            var decArr = ret.split(",");
            if (decArr.length > 1) {
                var dec = decArr[1].length;
                if (dec === 1) { ret += "0"; }
            }
        }
        return ret;
    }
}