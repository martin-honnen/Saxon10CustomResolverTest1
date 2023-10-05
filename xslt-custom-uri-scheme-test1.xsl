<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="3.0">
  <xsl:template match="/">
    <xsl:sequence select="document('custom-scheme://test')"/>
  </xsl:template>
</xsl:stylesheet>