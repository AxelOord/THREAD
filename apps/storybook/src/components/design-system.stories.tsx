import type { Meta, StoryObj } from '@storybook/react';
import { DesignSystem } from '@thread/design-system';

const meta: Meta<typeof DesignSystem> = {
  component: DesignSystem,
  parameters: {
    layout: 'fullscreen',
  },
};

export default meta;

type Story = StoryObj<typeof DesignSystem>;

export const Default: Story = {
  args: {},
};
