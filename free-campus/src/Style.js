import { reactive, toRaw } from 'vue';
import { style as baseStyle } from '@vc/Style';

const rawBaseStyle = toRaw(baseStyle);

export const style = reactive({
  ...structuredClone(rawBaseStyle),
});
