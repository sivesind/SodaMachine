Tests use 
- XUnit as testframework: https://xunit.net/docs/getting-started/netcore/visual-studio
- Fluent Assertions to assist clear and simple asserts: https://fluentassertions.com/introduction

SodaMachine.Test can test intern classes ffrom the main lib due to this snippet in the .csproj of SodaMachine:

  <!-- Internal classes made visible to testproject -->
  <ItemGroup>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
      <_Parameter1>SodaMachine.Test</_Parameter1>
    </AssemblyAttribute>
  </ItemGroup>



