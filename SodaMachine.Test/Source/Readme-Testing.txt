Tests use 
- XUnit as testframework: https://xunit.net/docs/getting-started/netcore/visual-studio
- Fluent Assertions to assist clear and simple asserts: https://fluentassertions.com/introduction
- NSubstitute are used for mocking: https://nsubstitute.github.io/

SodaMachine.Test can test internal classes ffrom the main lib due to this snippet in the .csproj of SodaMachine:

  <!-- Internal classes made visible to testproject -->
  <ItemGroup>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
      <_Parameter1>SodaMachine.Test</_Parameter1>
    </AssemblyAttribute>
  </ItemGroup>



