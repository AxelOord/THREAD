import type { StorybookConfig } from '@storybook/nextjs';
import path from 'path';

const config: StorybookConfig = {
  stories: ['../**/*.@(mdx|stories.@(js|jsx|ts|tsx))'],
  addons: ['@storybook/addon-essentials', '@storybook/addon-interactions'],
  framework: {
    name: '@storybook/nextjs',
    options: {},
  },
  previewAnnotations: ['./.storybook/preview.ts'],
  webpackFinal: async (config) => {
    config.resolve = config.resolve || {};
    config.resolve.alias = {
      ...(config.resolve.alias || {}),
      '@thread/design-system': path.resolve(__dirname, '../../../libs/design-system/src/index.ts'),
    };
    return config;
  },
};

export default config;


// To customize your webpack configuration you can use the webpackFinal field.
// Check https://storybook.js.org/docs/react/builders/webpack#extending-storybooks-webpack-config
// and https://nx.dev/recipes/storybook/custom-builder-configs
