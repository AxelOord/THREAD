import {
  Tree,
  formatFiles,
  generateFiles,
  joinPathFragments,
} from '@nx/devkit';
import { CreateModuleGeneratorSchema } from './schema';
import { execSync } from 'child_process';
import { rmSync, existsSync } from 'fs';
import { join } from 'path';

const layers = ['Application', 'Domain', 'Infrastructure', 'Persistence', 'Presentation'];

export default async function (tree: Tree, options: CreateModuleGeneratorSchema) {
  const moduleName = options.name;

  for (const layerName of layers) {
    const projectName = `${moduleName}.${layerName}`;
    const relativePath = `backend/src/module/${moduleName}/${projectName}`;
    const namespace = `Thread.${moduleName}.${layerName}`;

    // Generate using @nx-dotnet/core:app
    execSync(
      `npx nx g @nx-dotnet/core:app --name ${projectName} --directory backend\\src\\modules\\${moduleName} --solutionFile apps\\backend\\Backend.sln --namespaceName ${namespace} --pathScheme nx --template classlib --testTemplate none --language C#`,
      { stdio: 'inherit' }
    );

    const absPath = join(tree.root, relativePath);

    // Clean up
    const objPath = join(absPath, 'obj');
    if (existsSync(objPath)) rmSync(objPath, { recursive: true, force: true });

    const class1Path = join(absPath, 'Class1.cs');
    if (existsSync(class1Path)) rmSync(class1Path, { force: true });

    // Add AssemblyReference.cs from template
    generateFiles(
      tree,
      joinPathFragments(__dirname, 'files/src'),
      joinPathFragments(relativePath),
      {
        moduleName,
        layerName,
        namespace,
        tmpl: '', // required to prevent .__template__ suffix
      }
    );
  }

  await formatFiles(tree);
}
